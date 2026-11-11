defmodule LiteLLM.LiteLLMBackgroundResponseSettlement do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          response_id: String.t(),
          settled_at: DateTime.t()
        }

  schema "LiteLLM_BackgroundResponseSettlement" do
    field(:response_id, :string, primary_key: true)
    field(:settled_at, LiteLLM.PrismaDateTime, autogenerate: true)
  end
end