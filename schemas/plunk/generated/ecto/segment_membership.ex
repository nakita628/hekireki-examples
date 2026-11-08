defmodule Plunk.SegmentMembership do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Plunk.PrismaDateTime, autogenerate: {Plunk.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          contact_id: Ecto.UUID.t(),
          segment_id: Ecto.UUID.t(),
          entered_at: DateTime.t(),
          exited_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          contact: Plunk.Contact.t() | nil,
          segment: Plunk.Segment.t() | nil
        }

  schema "segment_memberships" do
    field(:entered_at, Plunk.PrismaDateTime, autogenerate: true, source: :enteredAt)
    field(:exited_at, Plunk.PrismaDateTime, source: :exitedAt)
    field(:contact_id, :binary_id, primary_key: true, source: :contactId)
    field(:segment_id, :binary_id, primary_key: true, source: :segmentId)
    belongs_to(:contact, Plunk.Contact, foreign_key: :contact_id, define_field: false, type: :binary_id)
    belongs_to(:segment, Plunk.Segment, foreign_key: :segment_id, define_field: false, type: :binary_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end