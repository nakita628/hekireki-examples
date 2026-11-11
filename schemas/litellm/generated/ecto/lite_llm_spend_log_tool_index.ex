defmodule LiteLLM.LiteLLMSpendLogToolIndex do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          request_id: String.t(),
          tool_name: String.t(),
          start_time: DateTime.t()
        }

  schema "LiteLLM_SpendLogToolIndex" do
    field(:request_id, :string, primary_key: true)
    field(:tool_name, :string, primary_key: true)
    field(:start_time, LiteLLM.PrismaDateTime)
  end
end