defmodule LiteLLM.LiteLLMAutoRouterBaselineComparison do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          scope: String.t(),
          api_key: String.t(),
          session_id: String.t(),
          router_name: String.t(),
          initial_equivalent: boolean(),
          revision: integer(),
          published_revision: integer(),
          history: String.t() | nil,
          attempted_at: DateTime.t() | nil,
          retired: boolean(),
          updated_at: DateTime.t()
        }

  schema "LiteLLM_AutoRouterBaselineComparison" do
    field(:scope, :string, primary_key: true)
    field(:api_key, :string)
    field(:session_id, :string)
    field(:router_name, :string)
    field(:initial_equivalent, :boolean)
    field(:revision, :integer, default: 0)
    field(:published_revision, :integer, default: 0)
    field(:history, :string)
    field(:attempted_at, LiteLLM.PrismaDateTime)
    field(:retired, :boolean, default: false)
    field(:updated_at, LiteLLM.PrismaDateTime, autogenerate: true)
  end
end