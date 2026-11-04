defmodule Formbricks.OutboundEmailAuditErasureIntent do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: Formbricks.PrismaDateTime, autogenerate: {Formbricks.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          user_id: String.t() | nil,
          user_hash: String.t(),
          recipient_mac: String.t(),
          activated_at: DateTime.t() | nil,
          checked_at: DateTime.t() | nil,
          expires_at: DateTime.t(),
          inserted_at: DateTime.t()
        }

  schema "OutboundEmailAuditErasureIntent" do
    field(:user_id, :string, source: :userId)
    field(:user_hash, :string, source: :userHash)
    field(:recipient_mac, :string, source: :recipientMac)
    field(:activated_at, Formbricks.PrismaDateTime, source: :activatedAt)
    field(:checked_at, Formbricks.PrismaDateTime, source: :checkedAt)
    field(:expires_at, Formbricks.PrismaDateTime, source: :expiresAt)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end