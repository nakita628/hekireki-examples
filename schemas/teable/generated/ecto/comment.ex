defmodule Teable.Comment do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Teable.PrismaDateTime, autogenerate: {Teable.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          table_id: String.t(),
          record_id: String.t(),
          quote_id: String.t() | nil,
          content: String.t() | nil,
          reaction: String.t() | nil,
          deleted_time: DateTime.t() | nil,
          created_time: DateTime.t(),
          created_by: String.t(),
          updated_at: DateTime.t() | nil
        }

  schema "comment" do
    field(:id, :string, primary_key: true)
    field(:table_id, :string)
    field(:record_id, :string)
    field(:quote_id, :string, source: :quote_Id)
    field(:content, :string)
    field(:reaction, :string)
    field(:deleted_time, Teable.PrismaDateTime)
    field(:created_time, Teable.PrismaDateTime, autogenerate: true)
    field(:created_by, :string)
    timestamps(inserted_at: false, updated_at_source: :last_modified_time)
  end
end