defmodule InboxZero.ExecutedRule do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          thread_id: String.t(),
          message_id: String.t(),
          status: atom(),
          automated: boolean(),
          reason: String.t() | nil,
          match_metadata: map() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          rule: InboxZero.Rule.t() | nil,
          email_account: InboxZero.EmailAccount.t() | nil,
          action_items: [InboxZero.ExecutedAction.t()],
          scheduled_actions: [InboxZero.ScheduledAction.t()]
        }

  schema "ExecutedRule" do
    field(:id, :string, primary_key: true)
    field(:thread_id, :string, source: :threadId)
    field(:message_id, :string, source: :messageId)
    field(:status, Ecto.Enum, values: [:APPLIED, :APPLYING, :REJECTED, :PENDING, :SKIPPED, :ERROR])
    field(:automated, :boolean)
    field(:reason, :string)
    field(:match_metadata, :map, source: :matchMetadata)
    field(:rule_id, :string, source: :ruleId)
    field(:email_account_id, :string, source: :emailAccountId)
    belongs_to(:rule, InboxZero.Rule, foreign_key: :rule_id, define_field: false, type: :string)
    belongs_to(:email_account, InboxZero.EmailAccount, foreign_key: :email_account_id, define_field: false, type: :string)
    has_many(:action_items, InboxZero.ExecutedAction, foreign_key: :executed_rule_id, references: :id)
    has_many(:scheduled_actions, InboxZero.ScheduledAction, foreign_key: :executed_rule_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end