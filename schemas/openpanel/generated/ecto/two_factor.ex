defmodule OpenPanel.TwoFactor do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: OpenPanel.PrismaDateTime, autogenerate: {OpenPanel.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          secret: String.t(),
          backup_codes: String.t(),
          verified: boolean(),
          failed_verification_count: integer(),
          locked_until: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: OpenPanel.User.t() | nil
        }

  schema "two_factor" do
    field(:id, :string, primary_key: true, read_after_writes: true)
    field(:secret, :string)
    field(:backup_codes, :string, source: :backupCodes)
    field(:verified, :boolean, default: true)
    field(:failed_verification_count, :integer, default: 0, source: :failedVerificationCount)
    field(:locked_until, OpenPanel.PrismaDateTime, source: :lockedUntil)
    field(:user_id, :string, source: :userId)
    belongs_to(:user, OpenPanel.User, foreign_key: :user_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end