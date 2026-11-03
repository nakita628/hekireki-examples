defmodule Calcom.DSyncTeamGroupMapping do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}

  @type t :: %__MODULE__{
          id: integer(),
          organization_id: integer(),
          group_name: String.t(),
          team: Calcom.Team.t() | nil,
          directory: Calcom.DSyncData.t() | nil
        }

  schema "DSyncTeamGroupMapping" do
    field(:organization_id, :integer, source: :organizationId)
    field(:group_name, :string, source: :groupName)
    field(:team_id, :id, source: :teamId)
    field(:directory_id, :string, source: :directoryId)
    belongs_to(:team, Calcom.Team, foreign_key: :team_id, define_field: false)
    belongs_to(:directory, Calcom.DSyncData, foreign_key: :directory_id, define_field: false, type: :string, references: :directory_id)
  end
end