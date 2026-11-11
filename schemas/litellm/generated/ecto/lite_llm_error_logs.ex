defmodule LiteLLM.LiteLLMErrorLogs do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true, source: :request_id}
  @foreign_key_type :binary_id

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          start_time: DateTime.t(),
          end_time: DateTime.t(),
          api_base: String.t(),
          model_group: String.t(),
          litellm_model_name: String.t(),
          model_id: String.t(),
          request_kwargs: map(),
          exception_type: String.t(),
          exception_string: String.t(),
          status_code: String.t()
        }

  schema "LiteLLM_ErrorLogs" do
    field(:start_time, LiteLLM.PrismaDateTime, source: :startTime)
    field(:end_time, LiteLLM.PrismaDateTime, source: :endTime)
    field(:api_base, :string, default: "")
    field(:model_group, :string, default: "")
    field(:litellm_model_name, :string, default: "")
    field(:model_id, :string, default: "")
    field(:request_kwargs, :map, default: %{})
    field(:exception_type, :string, default: "")
    field(:exception_string, :string, default: "")
    field(:status_code, :string, default: "")
  end
end