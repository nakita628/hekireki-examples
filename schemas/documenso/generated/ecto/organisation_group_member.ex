defmodule Documenso.OrganisationGroupMember do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          group: Documenso.OrganisationGroup.t() | nil,
          organisation_member: Documenso.OrganisationMember.t() | nil
        }

  schema "OrganisationGroupMember" do
    field(:id, :string, primary_key: true)
    field(:group_id, :string, source: :groupId)
    field(:organisation_member_id, :string, source: :organisationMemberId)
    belongs_to(:group, Documenso.OrganisationGroup, foreign_key: :group_id, define_field: false, type: :string)
    belongs_to(:organisation_member, Documenso.OrganisationMember, foreign_key: :organisation_member_id, define_field: false, type: :string)
  end
end