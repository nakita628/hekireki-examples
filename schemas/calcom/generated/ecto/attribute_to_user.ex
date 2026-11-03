defmodule Calcom.AttributeToUser do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: Calcom.PrismaDateTime, autogenerate: {Calcom.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          weight: integer() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t() | nil,
          member: Calcom.Membership.t() | nil,
          attribute_option: Calcom.AttributeOption.t() | nil,
          created_by: Calcom.User.t() | nil,
          created_by_dsync: Calcom.DSyncData.t() | nil,
          updated_by: Calcom.User.t() | nil,
          updated_by_dsync: Calcom.DSyncData.t() | nil
        }

  schema "AttributeToUser" do
    field(:weight, :integer)
    field(:member_id, :id, source: :memberId)
    field(:attribute_option_id, :binary_id, source: :attributeOptionId)
    field(:created_by_id, :id, source: :createdById)
    field(:created_by_dsync_id, :string, source: :createdByDSyncId)
    field(:updated_by_id, :id, source: :updatedById)
    field(:updated_by_dsync_id, :string, source: :updatedByDSyncId)
    belongs_to(:member, Calcom.Membership, foreign_key: :member_id, define_field: false, type: :id)
    belongs_to(:attribute_option, Calcom.AttributeOption, foreign_key: :attribute_option_id, define_field: false)
    belongs_to(:created_by, Calcom.User, foreign_key: :created_by_id, define_field: false, type: :id)
    belongs_to(:created_by_dsync, Calcom.DSyncData, foreign_key: :created_by_dsync_id, define_field: false, type: :string, references: :directory_id)
    belongs_to(:updated_by, Calcom.User, foreign_key: :updated_by_id, define_field: false, type: :id)
    belongs_to(:updated_by_dsync, Calcom.DSyncData, foreign_key: :updated_by_dsync_id, define_field: false, type: :string, references: :directory_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end