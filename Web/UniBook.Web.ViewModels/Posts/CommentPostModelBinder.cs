namespace UniBook.Web.ViewModels.Posts
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public class CommentPostModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            var value = valueProviderResult.FirstValue;

            var model = value.Split('.')[1];
            bindingContext.Result = ModelBindingResult.Success(model);
            return Task.CompletedTask;
        }
    }
}
