defmodule Rallly.QueuedEmail do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Rallly.PrismaDateTime, autogenerate: {Rallly.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          kind: String.t(),
          subject_id: String.t(),
          batch_id: String.t(),
          status: atom(),
          send_after: DateTime.t() | nil,
          attempts: integer(),
          claimed_at: DateTime.t() | nil,
          sent_at: DateTime.t() | nil,
          last_error: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: Rallly.User.t() | nil
        }

  schema "queued_emails" do
    field(:id, :string, primary_key: true)
    field(:kind, :string)
    field(:subject_id, :string)
    field(:batch_id, :string)
    field(:status, Ecto.Enum, values: [:pending, :sent, :skipped, :failed], default: :pending)
    field(:send_after, Rallly.PrismaDateTime)
    field(:attempts, :integer, default: 0)
    field(:claimed_at, Rallly.PrismaDateTime)
    field(:sent_at, Rallly.PrismaDateTime)
    field(:last_error, :string)
    belongs_to(:user, Rallly.User, foreign_key: :user_id, type: :string)
    timestamps(inserted_at_source: :created_at)
  end
end