defmodule InboxZero.SnoozedThread do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          thread_id: String.t(),
          scheduled_for: DateTime.t(),
          status: atom(),
          client_mutation_id: String.t() | nil,
          updated_at: DateTime.t(),
          email_account: InboxZero.EmailAccount.t() | nil
        }

  schema "SnoozedThread" do
    field(:id, :string, primary_key: true)
    field(:thread_id, :string, source: :threadId)
    field(:scheduled_for, InboxZero.PrismaDateTime, source: :scheduledFor)
    field(:status, Ecto.Enum, values: [:PREPARING, :PENDING, :EXECUTING, :COMPLETED, :CANCELLED], default: :PENDING)
    field(:client_mutation_id, :string, source: :clientMutationId)
    field(:email_account_id, :string, source: :emailAccountId)
    belongs_to(:email_account, InboxZero.EmailAccount, foreign_key: :email_account_id, define_field: false, type: :string)
    timestamps(inserted_at: false, updated_at_source: :updatedAt)
  end
end