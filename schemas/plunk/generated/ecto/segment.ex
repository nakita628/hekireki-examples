defmodule Plunk.Segment do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: Plunk.PrismaDateTime, autogenerate: {Plunk.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          name: String.t(),
          description: String.t() | nil,
          type: atom(),
          condition: map() | nil,
          track_membership: boolean(),
          member_count: integer(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: Plunk.Project.t() | nil,
          memberships: [Plunk.SegmentMembership.t()],
          campaigns: [Plunk.Campaign.t()]
        }

  schema "segments" do
    field(:name, :string)
    field(:description, :string)
    field(:type, Ecto.Enum, values: [:DYNAMIC, :STATIC], default: :DYNAMIC)
    field(:condition, :map)
    field(:track_membership, :boolean, default: false, source: :trackMembership)
    field(:member_count, :integer, default: 0, source: :memberCount)
    field(:project_id, :binary_id, source: :projectId)
    belongs_to(:project, Plunk.Project, foreign_key: :project_id, define_field: false)
    has_many(:memberships, Plunk.SegmentMembership, foreign_key: :segment_id)
    has_many(:campaigns, Plunk.Campaign, foreign_key: :segment_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end