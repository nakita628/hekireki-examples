defmodule LiteLLM.LiteLLMModelTable do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: LiteLLM.PrismaDateTime, autogenerate: {LiteLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          model_aliases: map() | nil,
          created_by: String.t(),
          updated_by: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          team: LiteLLM.LiteLLMTeamTable.t() | nil
        }

  schema "LiteLLM_ModelTable" do
    field(:model_aliases, :map, source: :aliases)
    field(:created_by, :string)
    field(:updated_by, :string)
    has_one(:team, LiteLLM.LiteLLMTeamTable, foreign_key: :model_id)
    timestamps(inserted_at_source: :created_at)
  end
end