defmodule InboxZero.ReplyMemory do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          content: String.t(),
          kind: atom(),
          scope_type: atom(),
          scope_value: String.t(),
          is_learned_style_evidence: boolean(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          email_account: InboxZero.EmailAccount.t() | nil,
          sources: [InboxZero.ReplyMemorySource.t()]
        }

  schema "ReplyMemory" do
    field(:id, :string, primary_key: true)
    field(:content, :string)
    field(:kind, Ecto.Enum, values: [:FACT, :PREFERENCE, :PROCEDURE])
    field(:scope_type, Ecto.Enum, values: [:GLOBAL, :SENDER, :DOMAIN, :TOPIC], source: :scopeType)
    field(:scope_value, :string, source: :scopeValue)
    field(:is_learned_style_evidence, :boolean, default: false, source: :isLearnedStyleEvidence)
    field(:email_account_id, :string, source: :emailAccountId)
    belongs_to(:email_account, InboxZero.EmailAccount, foreign_key: :email_account_id, define_field: false, type: :string)
    has_many(:sources, InboxZero.ReplyMemorySource, foreign_key: :reply_memory_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end