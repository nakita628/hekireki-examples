defmodule LiteLLM.LiteLLMConfig do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          param_name: String.t(),
          param_value: map() | nil,
          last_run_at: DateTime.t() | nil,
          reload_revision: integer()
        }

  schema "LiteLLM_Config" do
    field(:param_name, :string, primary_key: true)
    field(:param_value, :map)
    field(:last_run_at, LiteLLM.PrismaDateTime)
    field(:reload_revision, :integer, default: 0)
  end
end