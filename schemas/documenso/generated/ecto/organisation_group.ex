defmodule Documenso.OrganisationGroup do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t() | nil,
          type: atom(),
          organisation_role: atom(),
          organisation: Documenso.Organisation.t() | nil,
          organisation_group_members: [Documenso.OrganisationGroupMember.t()],
          team_groups: [Documenso.TeamGroup.t()]
        }

  schema "OrganisationGroup" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:type, Ecto.Enum, values: [:INTERNAL_ORGANISATION, :INTERNAL_TEAM, :CUSTOM])
    field(:organisation_role, Ecto.Enum, values: [:ADMIN, :MANAGER, :MEMBER], source: :organisationRole)
    field(:organisation_id, :string, source: :organisationId)
    belongs_to(:organisation, Documenso.Organisation, foreign_key: :organisation_id, define_field: false, type: :string)
    has_many(:organisation_group_members, Documenso.OrganisationGroupMember, foreign_key: :group_id)
    has_many(:team_groups, Documenso.TeamGroup, foreign_key: :organisation_group_id)
  end
end