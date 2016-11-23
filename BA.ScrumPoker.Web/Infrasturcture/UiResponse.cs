
namespace BA.ScrumPoker.Infrasturcture
{
	public class UiResponse<T>
	{
		public T Data { get; set; }
		public ErrorMessage Error { get; set; }

		public UiResponse(T data)
		{
			Data = data;
			Error = new ErrorMessage()
			{
				HasError = false
			};
		}

		public UiResponse(string errorMessage)
		{
			Error = new ErrorMessage()
			{
				HasError = true,
				Message = errorMessage
			};
		}
	}

	public class ErrorMessage
	{
		public string Message { get; set; }
		public bool HasError { get; set; }
	}
}