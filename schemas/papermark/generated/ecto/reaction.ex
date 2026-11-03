defmodule Papermark.Reaction do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Papermark.PrismaDateTime, autogenerate: {Papermark.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          page_number: integer(),
          type: String.t(),
          inserted_at: DateTime.t(),
          view: Papermark.View.t() | nil
        }

  schema "Reaction" do
    field(:id, :string, primary_key: true)
    field(:page_number, :integer, source: :pageNumber)
    field(:type, :string)
    field(:view_id, :string, source: :viewId)
    belongs_to(:view, Papermark.View, foreign_key: :view_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end