defmodule LangWatch.UserErasure do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          account_created_at: DateTime.t(),
          erased_at: DateTime.t()
        }

  schema "UserErasure" do
    field(:id, :string, primary_key: true)
    field(:account_created_at, LangWatch.PrismaDateTime, source: :accountCreatedAt)
    field(:erased_at, LangWatch.PrismaDateTime, autogenerate: true, source: :erasedAt)
  end
end