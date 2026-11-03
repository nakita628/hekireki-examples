defmodule Teable.Attachments do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          token: String.t(),
          hash: String.t(),
          size: integer(),
          mimetype: String.t(),
          path: String.t(),
          width: integer() | nil,
          height: integer() | nil,
          deleted_time: DateTime.t() | nil,
          created_time: DateTime.t(),
          created_by: String.t(),
          last_modified_by: String.t() | nil,
          thumbnail_path: String.t() | nil
        }

  schema "attachments" do
    field(:id, :string, primary_key: true)
    field(:token, :string)
    field(:hash, :string)
    field(:size, :integer)
    field(:mimetype, :string)
    field(:path, :string)
    field(:width, :integer)
    field(:height, :integer)
    field(:deleted_time, Teable.PrismaDateTime)
    field(:created_time, Teable.PrismaDateTime, autogenerate: true)
    field(:created_by, :string)
    field(:last_modified_by, :string)
    field(:thumbnail_path, :string)
  end
end