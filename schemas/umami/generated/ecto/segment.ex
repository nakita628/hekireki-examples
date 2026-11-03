defmodule Umami.Segment do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Umami.PrismaDateTime, autogenerate: {Umami.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          type: String.t(),
          name: String.t(),
          parameters: map(),
          inserted_at: DateTime.t() | nil,
          updated_at: DateTime.t() | nil,
          website: Umami.Website.t() | nil
        }

  schema "segment" do
    field(:id, :string, primary_key: true, source: :segment_id)
    field(:type, :string)
    field(:name, :string)
    field(:parameters, :map)
    belongs_to(:website, Umami.Website, foreign_key: :website_id, type: :string)
    timestamps(inserted_at_source: :created_at)
  end
end