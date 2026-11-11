defmodule LiteLLM.LiteLLMConfigOverrides do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LiteLLM.PrismaDateTime, autogenerate: {LiteLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          config_type: String.t(),
          config_value: map(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "LiteLLM_ConfigOverrides" do
    field(:config_type, :string, primary_key: true)
    field(:config_value, :map)
    timestamps(inserted_at_source: :created_at)
  end
end