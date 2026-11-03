defmodule Calcom.AttributeSyncRule do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: Calcom.PrismaDateTime, autogenerate: {Calcom.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          rule: map(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          integration_attribute_sync: Calcom.IntegrationAttributeSync.t() | nil
        }

  schema "AttributeSyncRule" do
    field(:rule, :map)
    field(:integration_attribute_sync_id, :binary_id, source: :integrationAttributeSyncId)
    belongs_to(:integration_attribute_sync, Calcom.IntegrationAttributeSync, foreign_key: :integration_attribute_sync_id, define_field: false)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end