defmodule InboxZero.EmailSendOperation do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          client_mutation_id: String.t(),
          payload_hash: String.t(),
          status: atom(),
          processing_started_at: DateTime.t(),
          result: map() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          email_account: InboxZero.EmailAccount.t() | nil
        }

  schema "EmailSendOperation" do
    field(:id, :string, primary_key: true)
    field(:client_mutation_id, :string, source: :clientMutationId)
    field(:payload_hash, :string, source: :payloadHash)
    field(:status, Ecto.Enum, values: [:PROCESSING, :SENT, :UNCERTAIN], default: :PROCESSING)
    field(:processing_started_at, InboxZero.PrismaDateTime, autogenerate: true, source: :processingStartedAt)
    field(:result, :map)
    field(:email_account_id, :string, source: :emailAccountId)
    belongs_to(:email_account, InboxZero.EmailAccount, foreign_key: :email_account_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end