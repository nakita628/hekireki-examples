defmodule Calcom.Attribute do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: Calcom.PrismaDateTime, autogenerate: {Calcom.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          type: atom(),
          name: String.t(),
          slug: String.t(),
          enabled: boolean(),
          users_can_edit_relation: boolean(),
          is_weights_enabled: boolean(),
          is_locked: boolean(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          team: Calcom.Team.t() | nil,
          options: [Calcom.AttributeOption.t()],
          attribute_sync_field_mappings: [Calcom.AttributeSyncFieldMapping.t()]
        }

  schema "Attribute" do
    field(:type, Ecto.Enum, values: [:TEXT, :NUMBER, :SINGLE_SELECT, :MULTI_SELECT])
    field(:name, :string)
    field(:slug, :string)
    field(:enabled, :boolean, default: true)
    field(:users_can_edit_relation, :boolean, default: false, source: :usersCanEditRelation)
    field(:is_weights_enabled, :boolean, default: false, source: :isWeightsEnabled)
    field(:is_locked, :boolean, default: false, source: :isLocked)
    field(:team_id, :id, source: :teamId)
    belongs_to(:team, Calcom.Team, foreign_key: :team_id, define_field: false, type: :id)
    has_many(:options, Calcom.AttributeOption, foreign_key: :attribute_id)
    has_many(:attribute_sync_field_mappings, Calcom.AttributeSyncFieldMapping, foreign_key: :attribute_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end