defmodule InboxZero.CalendarConnection do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          provider: String.t(),
          email: String.t(),
          access_token: String.t() | nil,
          refresh_token: String.t() | nil,
          expires_at: DateTime.t() | nil,
          is_connected: boolean(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          email_account: InboxZero.EmailAccount.t() | nil,
          calendars: [InboxZero.Calendar.t()]
        }

  schema "CalendarConnection" do
    field(:id, :string, primary_key: true)
    field(:provider, :string)
    field(:email, :string)
    field(:access_token, :string, source: :accessToken)
    field(:refresh_token, :string, source: :refreshToken)
    field(:expires_at, InboxZero.PrismaDateTime, source: :expiresAt)
    field(:is_connected, :boolean, default: true, source: :isConnected)
    field(:email_account_id, :string, source: :emailAccountId)
    belongs_to(:email_account, InboxZero.EmailAccount, foreign_key: :email_account_id, define_field: false, type: :string)
    has_many(:calendars, InboxZero.Calendar, foreign_key: :connection_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end