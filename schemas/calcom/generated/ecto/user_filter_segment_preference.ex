defmodule Calcom.UserFilterSegmentPreference do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: Calcom.PrismaDateTime, autogenerate: {Calcom.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          table_identifier: String.t(),
          system_segment_id: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: Calcom.User.t() | nil,
          segment: Calcom.FilterSegment.t() | nil
        }

  schema "UserFilterSegmentPreference" do
    field(:table_identifier, :string, source: :tableIdentifier)
    field(:system_segment_id, :string, source: :systemSegmentId)
    field(:user_id, :id, source: :userId)
    field(:segment_id, :id, source: :segmentId)
    belongs_to(:user, Calcom.User, foreign_key: :user_id, define_field: false)
    belongs_to(:segment, Calcom.FilterSegment, foreign_key: :segment_id, define_field: false)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end