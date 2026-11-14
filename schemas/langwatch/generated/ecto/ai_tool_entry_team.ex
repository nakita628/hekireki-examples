defmodule LangWatch.AiToolEntryTeam do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          entry: LangWatch.AiToolEntry.t() | nil,
          team: LangWatch.Team.t() | nil
        }

  schema "AiToolEntryTeam" do
    field(:id, :string, primary_key: true)
    field(:entry_id, :string, source: :entryId)
    field(:team_id, :string, source: :teamId)
    belongs_to(:entry, LangWatch.AiToolEntry, foreign_key: :entry_id, define_field: false, type: :string)
    belongs_to(:team, LangWatch.Team, foreign_key: :team_id, define_field: false, type: :string)
  end
end