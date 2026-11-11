defmodule InboxZero.MailSplitFilter do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          kind: atom(),
          value: String.t() | nil,
          order: integer(),
          mail_split: InboxZero.MailSplit.t() | nil
        }

  schema "MailSplitFilter" do
    field(:id, :string, primary_key: true)
    field(:kind, Ecto.Enum, values: [:UNREAD, :STARRED, :LABEL, :CATEGORY, :FROM, :OLDER_THAN, :TO])
    field(:value, :string)
    field(:order, :integer)
    field(:mail_split_id, :string, source: :mailSplitId)
    belongs_to(:mail_split, InboxZero.MailSplit, foreign_key: :mail_split_id, define_field: false, type: :string)
  end
end