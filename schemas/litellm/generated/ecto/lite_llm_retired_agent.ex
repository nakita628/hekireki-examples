defmodule LiteLLM.LiteLLMRetiredAgent do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          original_agent_id: String.t(),
          retired_at: DateTime.t()
        }

  schema "LiteLLM_RetiredAgent" do
    field(:original_agent_id, :string, primary_key: true)
    field(:retired_at, LiteLLM.PrismaDateTime, autogenerate: true)
  end
end