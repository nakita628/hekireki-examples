defmodule Calcom.OutOfOfficeEntry do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: Calcom.PrismaDateTime, autogenerate: {Calcom.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          uuid: String.t(),
          start: DateTime.t(),
          end: DateTime.t(),
          notes: String.t() | nil,
          show_note_publicly: boolean(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: Calcom.User.t() | nil,
          to_user: Calcom.User.t() | nil,
          reason: Calcom.OutOfOfficeReason.t() | nil
        }

  schema "OutOfOfficeEntry" do
    field(:uuid, :string)
    field(:start, Calcom.PrismaDateTime)
    field(:end, Calcom.PrismaDateTime)
    field(:notes, :string)
    field(:show_note_publicly, :boolean, default: false, source: :showNotePublicly)
    field(:user_id, :id, source: :userId)
    field(:to_user_id, :id, source: :toUserId)
    field(:reason_id, :id, source: :reasonId)
    belongs_to(:user, Calcom.User, foreign_key: :user_id, define_field: false)
    belongs_to(:to_user, Calcom.User, foreign_key: :to_user_id, define_field: false)
    belongs_to(:reason, Calcom.OutOfOfficeReason, foreign_key: :reason_id, define_field: false)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end