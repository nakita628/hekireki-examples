defmodule Teable.ShortLink do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          code: String.t(),
          type: String.t(),
          resource_id: String.t(),
          created_time: DateTime.t(),
          created_by: String.t(),
          deleted_time: DateTime.t() | nil
        }

  schema "short_link" do
    field(:id, :string, primary_key: true)
    field(:code, :string)
    field(:type, :string)
    field(:resource_id, :string)
    field(:created_time, Teable.PrismaDateTime, autogenerate: true)
    field(:created_by, :string)
    field(:deleted_time, Teable.PrismaDateTime)
  end
end