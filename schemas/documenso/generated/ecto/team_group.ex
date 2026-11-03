defmodule Documenso.TeamGroup do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          team_role: atom(),
          organisation_group: Documenso.OrganisationGroup.t() | nil,
          team: Documenso.Team.t() | nil
        }

  schema "TeamGroup" do
    field(:id, :string, primary_key: true)
    field(:team_role, Ecto.Enum, values: [:ADMIN, :MANAGER, :MEMBER], source: :teamRole)
    field(:organisation_group_id, :string, source: :organisationGroupId)
    field(:team_id, :id, source: :teamId)
    belongs_to(:organisation_group, Documenso.OrganisationGroup, foreign_key: :organisation_group_id, define_field: false, type: :string)
    belongs_to(:team, Documenso.Team, foreign_key: :team_id, define_field: false)
  end
end