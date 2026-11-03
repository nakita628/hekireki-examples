defmodule Umami.EventData do
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
          inserted_at: DateTime.t() | nil,
          website: Umami.Website.t() | nil,
          website_event: Umami.WebsiteEvent.t() | nil
        }

  schema "event_data" do
    field(:id, :string, primary_key: true, source: :event_data_id)
    field(:data_key, :string)
    field(:string_value, :string)
    field(:number_value, :decimal)
    field(:date_value, Umami.PrismaDateTime)
    field(:data_type, :integer)
    belongs_to(:website, Umami.Website, foreign_key: :website_id, type: :string)
    belongs_to(:website_event, Umami.WebsiteEvent, foreign_key: :website_event_id, type: :string)
    timestamps(inserted_at_source: :created_at, updated_at: false)
  end
end