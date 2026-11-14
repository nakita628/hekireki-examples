defmodule LangWatch.SignInAttemptLock do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          identifier_hash: String.t(),
          failed_count: integer(),
          locked_until: DateTime.t() | nil,
          consecutive_lockouts: integer(),
          held_for_review: boolean(),
          user_id: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "SignInAttemptLock" do
    field(:id, :string, primary_key: true)
    field(:identifier_hash, :string, source: :identifierHash)
    field(:failed_count, :integer, default: 0, source: :failedCount)
    field(:locked_until, LangWatch.PrismaDateTime, source: :lockedUntil)
    field(:consecutive_lockouts, :integer, default: 0, source: :consecutiveLockouts)
    field(:held_for_review, :boolean, default: false, source: :heldForReview)
    field(:user_id, :string, source: :userId)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end