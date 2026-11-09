defmodule Jitsu.EmailLog do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          time: DateTime.t(),
          email: map(),
          error: String.t() | nil,
          preview_url: String.t() | nil,
          message_id: String.t() | nil,
          status: String.t()
        }

  schema "EmailLog" do
    field(:id, :string, primary_key: true)
    field(:time, Jitsu.PrismaDateTime, autogenerate: true)
    field(:email, :map)
    field(:error, :string)
    field(:preview_url, :string, source: :previewUrl)
    field(:message_id, :string, source: :messageId)
    field(:status, :string)
  end
end