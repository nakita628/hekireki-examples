defmodule Calcom.IntegrationAttributeSync do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: Calcom.PrismaDateTime, autogenerate: {Calcom.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          name: String.t(),
          integration: String.t(),
          enabled: boolean(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          organization: Calcom.Team.t() | nil,
          credential: Calcom.Credential.t() | nil,
          attribute_sync_rule: Calcom.AttributeSyncRule.t() | nil,
          sync_field_mappings: [Calcom.AttributeSyncFieldMapping.t()]
        }

  schema "IntegrationAttributeSync" do
    field(:name, :string)
    field(:integration, :string)
    field(:enabled, :boolean)
    field(:organization_id, :id, source: :organizationId)
    field(:credential_id, :id, source: :credentialId)
    belongs_to(:organization, Calcom.Team, foreign_key: :organization_id, define_field: false, type: :id)
    belongs_to(:credential, Calcom.Credential, foreign_key: :credential_id, define_field: false, type: :id)
    has_one(:attribute_sync_rule, Calcom.AttributeSyncRule, foreign_key: :integration_attribute_sync_id)
    has_many(:sync_field_mappings, Calcom.AttributeSyncFieldMapping, foreign_key: :integration_attribute_sync_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end