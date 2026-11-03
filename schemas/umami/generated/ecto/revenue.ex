defmodule Umami.Revenue do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Umami.PrismaDateTime, autogenerate: {Umami.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          event_id: String.t(),
          event_name: String.t(),
          currency: String.t(),
          revenue: Decimal.t() | nil,
          inserted_at: DateTime.t() | nil,
          website: Umami.Website.t() | nil,
          session: Umami.Session.t() | nil
        }

  schema "revenue" do
    field(:id, :string, primary_key: true, source: :revenue_id)
    field(:event_id, :string)
    field(:event_name, :string)
    field(:currency, :string)
    field(:revenue, :decimal)
    belongs_to(:website, Umami.Website, foreign_key: :website_id, type: :string)
    belongs_to(:session, Umami.Session, foreign_key: :session_id, type: :string)
    timestamps(inserted_at_source: :created_at, updated_at: false)
  end
end