defmodule Teable.Account do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          type: String.t(),
          provider: String.t(),
          provider_id: String.t(),
          connection_id: String.t(),
          created_time: DateTime.t(),
          user: Teable.User.t() | nil
        }

  schema "account" do
    field(:id, :string, primary_key: true)
    field(:type, :string)
    field(:provider, :string)
    field(:provider_id, :string)
    field(:connection_id, :string, default: "")
    field(:created_time, Teable.PrismaDateTime, autogenerate: true)
    belongs_to(:user, Teable.User, foreign_key: :user_id, type: :string)
  end
end