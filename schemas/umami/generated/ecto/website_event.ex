defmodule Umami.WebsiteEvent do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Umami.PrismaDateTime, autogenerate: {Umami.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          website_id: String.t(),
          visit_id: String.t(),
          url_path: String.t(),
          url_query: String.t() | nil,
          utm_source: String.t() | nil,
          utm_medium: String.t() | nil,
          utm_campaign: String.t() | nil,
          utm_content: String.t() | nil,
          utm_term: String.t() | nil,
          referrer_path: String.t() | nil,
          referrer_query: String.t() | nil,
          referrer_domain: String.t() | nil,
          page_title: String.t() | nil,
          gclid: String.t() | nil,
          fbclid: String.t() | nil,
          msclkid: String.t() | nil,
          ttclid: String.t() | nil,
          lifatid: String.t() | nil,
          twclid: String.t() | nil,
          event_type: integer(),
          event_name: String.t() | nil,
          tag: String.t() | nil,
          hostname: String.t() | nil,
          lcp: Decimal.t() | nil,
          inp: Decimal.t() | nil,
          cls: Decimal.t() | nil,
          fcp: Decimal.t() | nil,
          ttfb: Decimal.t() | nil,
          inserted_at: DateTime.t() | nil,
          session: Umami.Session.t() | nil,
          event_data: [Umami.EventData.t()]
        }

  schema "website_event" do
    field(:id, :string, primary_key: true, source: :event_id)
    field(:website_id, :string)
    field(:visit_id, :string)
    field(:url_path, :string)
    field(:url_query, :string)
    field(:utm_source, :string)
    field(:utm_medium, :string)
    field(:utm_campaign, :string)
    field(:utm_content, :string)
    field(:utm_term, :string)
    field(:referrer_path, :string)
    field(:referrer_query, :string)
    field(:referrer_domain, :string)
    field(:page_title, :string)
    field(:gclid, :string)
    field(:fbclid, :string)
    field(:msclkid, :string)
    field(:ttclid, :string)
    field(:lifatid, :string, source: :li_fat_id)
    field(:twclid, :string)
    field(:event_type, :integer, default: 1)
    field(:event_name, :string)
    field(:tag, :string)
    field(:hostname, :string)
    field(:lcp, :decimal)
    field(:inp, :decimal)
    field(:cls, :decimal)
    field(:fcp, :decimal)
    field(:ttfb, :decimal)
    belongs_to(:session, Umami.Session, foreign_key: :session_id, type: :string)
    has_many(:event_data, Umami.EventData, foreign_key: :website_event_id)
    timestamps(inserted_at_source: :created_at, updated_at: false)
  end
end