defmodule Teable.CommentSubscription do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          table_id: String.t(),
          record_id: String.t(),
          created_by: String.t(),
          created_time: DateTime.t()
        }

  schema "comment_subscription" do
    field(:id, :string, primary_key: true)
    field(:table_id, :string)
    field(:record_id, :string)
    field(:created_by, :string)
    field(:created_time, Teable.PrismaDateTime, autogenerate: true)
  end
end