defmodule LiteLLM.LiteLLMUISettings do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LiteLLM.PrismaDateTime, autogenerate: {LiteLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          ui_settings: map(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "LiteLLM_UISettings" do
    field(:id, :string, primary_key: true, default: "ui_settings")
    field(:ui_settings, :map)
    timestamps(inserted_at_source: :created_at)
  end
end