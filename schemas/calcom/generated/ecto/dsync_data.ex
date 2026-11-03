defmodule Calcom.DSyncData do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}

  @type t :: %__MODULE__{
          id: integer(),
          directory_id: String.t(),
          tenant: String.t(),
          org: Calcom.OrganizationSettings.t() | nil,
          team_group_mapping: [Calcom.DSyncTeamGroupMapping.t()],
          created_attribute_to_users: [Calcom.AttributeToUser.t()],
          updated_attribute_to_users: [Calcom.AttributeToUser.t()]
        }

  schema "DSyncData" do
    field(:directory_id, :string, source: :directoryId)
    field(:tenant, :string)
    field(:organization_id, :id, source: :organizationId)
    belongs_to(:org, Calcom.OrganizationSettings, foreign_key: :organization_id, define_field: false, references: :organization_id)
    has_many(:team_group_mapping, Calcom.DSyncTeamGroupMapping, foreign_key: :directory_id, references: :directory_id)
    has_many(:created_attribute_to_users, Calcom.AttributeToUser, foreign_key: :created_by_dsync_id, references: :directory_id)
    has_many(:updated_attribute_to_users, Calcom.AttributeToUser, foreign_key: :updated_by_dsync_id, references: :directory_id)
  end
end