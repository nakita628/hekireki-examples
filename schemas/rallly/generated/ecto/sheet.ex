defmodule Rallly.Sheet do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Rallly.PrismaDateTime, autogenerate: {Rallly.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          title: String.t(),
          description: String.t() | nil,
          url_id: String.t(),
          deleted: boolean(),
          deleted_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          space: Rallly.Space.t() | nil,
          host: Rallly.User.t() | nil,
          slots: [Rallly.SheetSlot.t()]
        }

  schema "sheets" do
    field(:id, :string, primary_key: true)
    field(:title, :string)
    field(:description, :string)
    field(:url_id, :string)
    field(:deleted, :boolean, default: false)
    field(:deleted_at, Rallly.PrismaDateTime)
    belongs_to(:space, Rallly.Space, foreign_key: :space_id, type: :binary_id)
    belongs_to(:host, Rallly.User, foreign_key: :host_id, type: :string)
    has_many(:slots, Rallly.SheetSlot, foreign_key: :sheet_id, references: :id)
    timestamps(inserted_at_source: :created_at)
  end
end