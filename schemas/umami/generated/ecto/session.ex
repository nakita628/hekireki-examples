defmodule Umami.Session do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Umami.PrismaDateTime, autogenerate: {Umami.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          website_id: String.t(),
          browser: String.t() | nil,
          os: String.t() | nil,
          device: String.t() | nil,
          screen: String.t() | nil,
          language: String.t() | nil,
          country: String.t() | nil,
          region: String.t() | nil,
          city: String.t() | nil,
          distinct_id: String.t() | nil,
          inserted_at: DateTime.t() | nil,
          website_events: [Umami.WebsiteEvent.t()],
          session_data: [Umami.SessionData.t()],
          revenue: [Umami.Revenue.t()]
        }

  schema "session" do
    field(:id, :string, primary_key: true, source: :session_id)
    field(:website_id, :string)
    field(:browser, :string)
    field(:os, :string)
    field(:device, :string)
    field(:screen, :string)
    field(:language, :string)
    field(:country, :string)
    field(:region, :string)
    field(:city, :string)
    field(:distinct_id, :string)
    has_many(:website_events, Umami.WebsiteEvent, foreign_key: :session_id)
    has_many(:session_data, Umami.SessionData, foreign_key: :session_id)
    has_many(:revenue, Umami.Revenue, foreign_key: :session_id)
    timestamps(inserted_at_source: :created_at, updated_at: false)
  end
end