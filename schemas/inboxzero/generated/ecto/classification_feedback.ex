defmodule InboxZero.ClassificationFeedback do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          sender: String.t(),
          event_type: atom(),
          thread_id: String.t(),
          message_id: String.t(),
          inserted_at: DateTime.t(),
          rule: InboxZero.Rule.t() | nil,
          email_account: InboxZero.EmailAccount.t() | nil
        }

  schema "ClassificationFeedback" do
    field(:id, :string, primary_key: true)
    field(:sender, :string)
    field(:event_type, Ecto.Enum, values: [:LABEL_ADDED, :LABEL_REMOVED], source: :eventType)
    field(:thread_id, :string, source: :threadId)
    field(:message_id, :string, source: :messageId)
    field(:rule_id, :string, source: :ruleId)
    field(:email_account_id, :string, source: :emailAccountId)
    belongs_to(:rule, InboxZero.Rule, foreign_key: :rule_id, define_field: false, type: :string)
    belongs_to(:email_account, InboxZero.EmailAccount, foreign_key: :email_account_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end