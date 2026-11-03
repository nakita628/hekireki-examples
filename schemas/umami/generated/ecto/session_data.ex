defmodule Umami.SessionData do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Umami.PrismaDateTime, autogenerate: {Umami.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          data_key: String.t(),
          string_value: String.t() | nil,
          number_value: Decimal.t() | nil,
          date_value: DateTime.t() | nil,
          data_type: integer(),
          distinct_id: String.t() | nil,
          inserted_at: DateTime.t() | nil,
          website: Umami.Website.t() | nil,
          session: Umami.Session.t() | nil
        }

  schema "session_data" do
    field(:id, :string, primary_key: true, source: :session_data_id)
    field(:data_key, :string)
    field(:string_value, :string)
    field(:number_value, :decimal)
    field(:date_value, Umami.PrismaDateTime)
    field(:data_type, :integer)
    field(:distinct_id, :string)
    belongs_to(:website, Umami.Website, foreign_key: :website_id, type: :string)
    belongs_to(:session, Umami.Session, foreign_key: :session_id, type: :string)
    timestamps(inserted_at_source: :created_at, updated_at: false)
  end
end