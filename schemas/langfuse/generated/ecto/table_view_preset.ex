defmodule Langfuse.TableViewPreset do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          table_name: String.t(),
          filters: map(),
          column_order: map(),
          column_visibility: map(),
          search_query: String.t() | nil,
          order_by: map() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: Langfuse.Project.t() | nil,
          created_by_user: Langfuse.User.t() | nil,
          updated_by_user: Langfuse.User.t() | nil
        }

  schema "table_view_presets" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:table_name, :string)
    field(:filters, :map)
    field(:column_order, :map)
    field(:column_visibility, :map)
    field(:search_query, :string)
    field(:order_by, :map)
    belongs_to(:project, Langfuse.Project, foreign_key: :project_id, type: :string)
    belongs_to(:created_by_user, Langfuse.User, foreign_key: :created_by, type: :string)
    belongs_to(:updated_by_user, Langfuse.User, foreign_key: :updated_by, type: :string)
    timestamps(inserted_at_source: :created_at)
  end
end