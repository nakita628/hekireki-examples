defmodule InboxZero.DigestItem do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          message_id: String.t(),
          thread_id: String.t(),
          content: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          digest: InboxZero.Digest.t() | nil,
          action: InboxZero.ExecutedAction.t() | nil,
          cold_email: InboxZero.ColdEmail.t() | nil
        }

  schema "DigestItem" do
    field(:id, :string, primary_key: true)
    field(:message_id, :string, source: :messageId)
    field(:thread_id, :string, source: :threadId)
    field(:content, :string)
    field(:digest_id, :string, source: :digestId)
    field(:action_id, :string, source: :actionId)
    field(:cold_email_id, :string, source: :coldEmailId)
    belongs_to(:digest, InboxZero.Digest, foreign_key: :digest_id, define_field: false, type: :string)
    belongs_to(:action, InboxZero.ExecutedAction, foreign_key: :action_id, define_field: false, type: :string)
    belongs_to(:cold_email, InboxZero.ColdEmail, foreign_key: :cold_email_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end