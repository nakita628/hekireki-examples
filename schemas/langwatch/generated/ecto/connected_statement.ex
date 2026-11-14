defmodule LangWatch.ConnectedStatement do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          account_id: String.t(),
          month: DateTime.t(),
          sent_at: DateTime.t()
        }

  schema "ConnectedStatement" do
    field(:id, :string, primary_key: true)
    field(:account_id, :string, source: :accountId)
    field(:month, LangWatch.PrismaDateTime)
    field(:sent_at, LangWatch.PrismaDateTime, source: :sentAt)
  end
end