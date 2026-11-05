defmodule OpenPanel.EmailUnsubscribe do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: OpenPanel.PrismaDateTime, autogenerate: {OpenPanel.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          email: String.t(),
          category: String.t(),
          inserted_at: DateTime.t()
        }

  schema "email_unsubscribes" do
    field(:id, :string, primary_key: true, read_after_writes: true)
    field(:email, :string)
    field(:category, :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end