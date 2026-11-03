defmodule Papermark.CustomField do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Papermark.PrismaDateTime, autogenerate: {Papermark.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          type: atom(),
          identifier: String.t(),
          label: String.t(),
          placeholder: String.t() | nil,
          required: boolean(),
          disabled: boolean(),
          order_index: integer(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          link: Papermark.Link.t() | nil
        }

  schema "CustomField" do
    field(:id, :string, primary_key: true)
    field(:type, Ecto.Enum, values: [:SHORT_TEXT, :LONG_TEXT, :NUMBER, :PHONE_NUMBER, :URL, :CHECKBOX, :SELECT, :MULTI_SELECT])
    field(:identifier, :string)
    field(:label, :string)
    field(:placeholder, :string)
    field(:required, :boolean, default: false)
    field(:disabled, :boolean, default: false)
    field(:order_index, :integer, default: 0, source: :orderIndex)
    field(:link_id, :string, source: :linkId)
    belongs_to(:link, Papermark.Link, foreign_key: :link_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end