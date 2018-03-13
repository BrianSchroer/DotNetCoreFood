using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Reflection;

namespace SparkyTestHelpers.AspNetCore.Mvc
{
    /// <summary>
    /// Helper for testing ASP.NET Core MVC controller actions.
    /// </summary>
    public class ControllerActionTester
    {
        private readonly Func<IActionResult> _controllerAction;
        private Type _expectedModelType;
        private string _expectedViewName;
        private bool _expectedViewNameSpecified = false;

        /// <summary>
        /// Private constructor - Only called by <see cref="ControllerActionTester.ForAction(Func{IActionResult})"/>.
        /// </summary>
        /// <param name="controllerAction"></param>
        private ControllerActionTester(Func<IActionResult> controllerAction)
        {
            _controllerAction = controllerAction;
        }

        /// <summary>
        /// Creates a new <see cref="ControllerActionTester"/> for the specified controller action.
        /// </summary>
        /// <param name="controllerAction">The controller action.</param>
        /// <returns></returns>
        public static ControllerActionTester ForAction(Func<IActionResult> controllerAction)
        {
            return new ControllerActionTester(controllerAction);
        }

        /// <summary>
        /// Specifies that the <see cref="Test{TActionResultType}"/> method should throw an exception
        /// if the action result's .ViewName doesn't match the <paramref name="expectedViewName"/>.
        /// </summary>
        /// <param name="expectedViewName">The expected view name.</param>
        /// <returns>"This" <see cref="ControllerActionTester"/>.</returns>
        public ControllerActionTester ExpectingViewName(string expectedViewName)
        {
            _expectedViewName = expectedViewName;
            _expectedViewNameSpecified = true;
            return this;
        }

        /// <summary>
        /// Specifies that the <see cref="Test{TActionResultType}"/> method should throw an exception
        /// if the action result's .Model type doesn't match <typeparamref name="TModelType"/>.
        /// </summary>
        /// <typeparam name="TModelType">The expected model type.</typeparam>
        /// <returns>"This" <see cref="ControllerActionTester"/>.</returns>
        public ControllerActionTester ExpectingModelType<TModelType>()
        {
            _expectedModelType = typeof(TModelType);
            return this;
        }

        /// <summary>
        /// Test the controller action.
        /// </summary>
        /// <typeparam name="TActionResultType">The expected <see cref="IActionResult"/> 
        /// type that should be returned from the action.</typeparam>
        /// <returns>The <typeparamref name="TActionResultType"/> returned from the controller action.</returns>
        /// <exception cref="MvcTestHelperException">if any errors are asserted.</exception>
        public TActionResultType Test<TActionResultType>() where TActionResultType : IActionResult
        {
            IActionResult actionResult = _controllerAction();

            AssertActionResultType<TActionResultType>(actionResult);

            if (_expectedViewNameSpecified)
            {
                AssertViewName(actionResult, _expectedViewName);
            }

            if (_expectedModelType != null)
            {
                AssertModelType(actionResult);
            }

            return (TActionResultType)actionResult;
        }

        /// <summary>
        /// Tests that controller action redirects to the specified action name.
        /// </summary>
        /// <param name="expectedActionName">The expected action name.</param>
        /// <returns>The <see cref="RedirectToActionResult"/> returned from the controller action.</returns>
        public RedirectToActionResult TestRedirectToAction(string expectedActionName)
        {
            IActionResult actionResult = _controllerAction();

            AssertActionResultType<RedirectToActionResult>(actionResult);

            var redirectResult = (RedirectToActionResult)actionResult;
            string actual = redirectResult.ActionName;

            if (!string.Equals(expectedActionName, actual, StringComparison.InvariantCulture))
            {
                throw new MvcTestHelperException($"Expected ActionName <{expectedActionName}>. Actual: <{actual}>.");
            }

            return redirectResult;
        }

        /// <summary>
        /// Tests that controller action redirects to the specified route.
        /// </summary>
        /// <param name="expectedRoute">The expected route.</param>
        /// <returns>The <see cref="RedirectToRouteResult"/> returned from the controller action.</returns>
        public RedirectToRouteResult TestRedirectToRoute(string expectedRoute)
        {
            IActionResult actionResult = _controllerAction();

            AssertActionResultType<RedirectToRouteResult>(actionResult);

            var redirectResult = (RedirectToRouteResult)actionResult;

            string actual = string.Join("/", redirectResult.RouteValues.Values.Select(v => v.ToString()));

            if (!string.Equals(expectedRoute, actual, StringComparison.InvariantCulture))
            {
                throw new MvcTestHelperException($"Expected route <{expectedRoute}>. Actual: <{actual}>.");
            }

            return redirectResult;
        }

        private void AssertModelType(IActionResult actionResult)
        {
            AssertIsViewResult(actionResult, nameof(AssertViewName));

            object model = ((ViewResult)actionResult)?.Model;

            if (!IsOfType(model, _expectedModelType))
            {
                throw new MvcTestHelperException(
                   $"Expected model type: {_expectedModelType.FullName}. Actual: {model.GetType().FullName}.");
            }
        }

        private void AssertActionResultType<TActionResultType>(IActionResult actionResult) where TActionResultType : IActionResult
        {
            Type expectedType = typeof(TActionResultType);

            if (!IsOfType(actionResult, expectedType))
            {
                throw new MvcTestHelperException(
                    $"Expected IActionResult type: {expectedType.FullName}. Actual: {actionResult.GetType().FullName}.");
            }
        }

        private void AssertIsViewResult(IActionResult actionResult, string methodName)
        {
            if (!IsOfType(actionResult, typeof(ViewResult)))
            {
                throw new MvcTestHelperException(
                    $"\"{methodName}\" is only appropriate for type ViewResult."
                        + " {nameof(actionResult)} is of type {actionResult.GetType().FullName}.");
            }
        }

        private void AssertViewName(IActionResult actionResult, string expectedViewName)
        {
            AssertIsViewResult(actionResult, nameof(AssertViewName));

            string actual = ((ViewResult)actionResult).ViewName;

            if (!string.Equals(expectedViewName, actual, StringComparison.InvariantCulture))
            {
                throw new MvcTestHelperException($"Expected ViewName <{expectedViewName}>. Actual: <{actual}>.");
            }
        }

        private static bool IsOfType(object obj, Type expectedType)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            Type actualType = obj.GetType();
            return actualType == expectedType || actualType.GetTypeInfo().IsSubclassOf(expectedType);
        }
    }
}
