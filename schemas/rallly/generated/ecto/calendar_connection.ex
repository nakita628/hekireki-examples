defmodule Rallly.CalendarConnection do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Rallly.PrismaDateTime, autogenerate: {Rallly.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          provider: String.t(),
          integration_id: String.t(),
          provider_account_id: String.t(),
          email: String.t(),
          display_name: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          credential: Rallly.Credential.t() | nil,
          user: Rallly.User.t() | nil,
          provider_calendars: [Rallly.ProviderCalendar.t()]
        }

  schema "calendar_connections" do
    field(:id, :string, primary_key: true)
    field(:provider, :string)
    field(:integration_id, :string)
    field(:provider_account_id, :string)
    field(:email, :string)
    field(:display_name, :string)
    belongs_to(:credential, Rallly.Credential, foreign_key: :credential_id, type: :string)
    belongs_to(:user, Rallly.User, foreign_key: :user_id, type: :string)
    has_many(:provider_calendars, Rallly.ProviderCalendar, foreign_key: :calendar_connection_id, references: :id)
    timestamps(inserted_at_source: :created_at)
  end
end